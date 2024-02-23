# Copyright (c) LumexUI 2024
# LumexUI licenses this file to you under the MIT license
# See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

import json
import requests
from pathlib import Path
from datetime import datetime
from typing import NamedTuple, Set
from joblib import Parallel, delayed


_METADATA_URL = (
    "http://fonts.google.com/metadata/icons?incomplete=1&key=material_symbols"
)


_URL_PATH = (
    "https://{host}/s/i/short-term/release/{family}/{icon.name}/default/{size_px}px.svg"
)


_FILE_DEST = "src/LumexUI/Icons/{family}.cs"


_FILE_TEMPLATE = """
//  =======================================================
//  |  This file was auto-generated. {timestamp}  |
//  =======================================================

// Copyright (c) UI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

namespace LumexUI;

public partial class Icons
{{
	public partial class {family}
    {{
""".lstrip()


class Icon(NamedTuple):
    name: str
    version: int
    families: Set[str]


class Fetch(NamedTuple):
    icon_name: str
    src_url: str


def _get_latest_metadata():
    try:
        resp = requests.get(_METADATA_URL)
        resp.raise_for_status()
        raw_json = resp.text[5:]
        return json.loads(raw_json)
    except Exception as e:
        print(str(e))


def _get_symbol_families(metadata):
    return set(s for s in set(metadata["families"]) if "Symbols" in s)


def _get_symbols(metadata):
    families = _get_symbol_families(metadata)
    for raw_icon in metadata["icons"]:
        unsupported_families = set(raw_icon["unsupported_families"])
        yield Icon(
            raw_icon["name"],
            raw_icon["version"],
            families - unsupported_families,
        )


def _url_pattern_args(metadata, icon, family):
    return {
        "host": metadata["host"],
        "family": family.replace(" ", "").lower(),
        "icon": icon,
        "size_px": 24,
    }


def _cleanup_svg(svg):
    start = svg.find(">")
    end = len("</svg>")
    return svg[start + 1 : -end]


def _create_fetch(icon, args):
    src_url = _URL_PATH.format(**args)
    icon_name = icon.name.replace("_", " ").title().replace(" ", "")
    if icon_name[0].isdigit():
        icon_name = "_" + icon_name
    return Fetch(icon_name, src_url)


def _create_fetches(fetches, family, icon, args):
    fetch = _create_fetch(icon, args)
    fetches[family].append(fetch)


def _create_consts(fetches):
    print(f"Start fetching process...")
    consts = {}

    for k, v in fetches.items():
        total = len(v)
        print(f"Fetching {total} items of {k} family...")
        k = k.split()[-1]
        consts[k] = _create_family_consts(v)
        print("%d/%d complete" % (total, total))
    return consts


def _create_family_consts(family_fetches):
    return Parallel(n_jobs=50)(
        _create_const_delayed(name, url) for name, url in family_fetches
    )


@delayed
def _create_const_delayed(icon_name, src_url):
    try:
        resp = requests.get(src_url)
        resp.raise_for_status()
        svg_path = _cleanup_svg(resp.text).replace('"', '\\"')
        return f'public const string {icon_name} = "{svg_path}";'
    except Exception as e:
        print(str(e))


def _create_csharp_file(family, consts):
    args = {
        "family": family,
        "timestamp": (datetime.now()).strftime("%d/%m/%Y %H:%M:%S"),
    }
    file_dest = (Path(__file__) / "../.." / _FILE_DEST.format(**args)).resolve()

    with open(file_dest, "w") as file:
        file.write(_FILE_TEMPLATE.format(**args))
        for const in consts:
            file.write(f"\t\t{const}\n")
        file.write("\t}\n}")


def main():
    metadata = _get_latest_metadata()
    families = _get_symbol_families(metadata)
    icons = tuple(_get_symbols(metadata))

    consts = {}
    fetches = {list: [] for list in families}

    for icon in icons:
        for family in families:
            if family not in icon.families:
                continue
            pattern_args = _url_pattern_args(metadata, icon, family)
            _create_fetches(fetches, family, icon, pattern_args)

    if fetches:
        consts = _create_consts(fetches)
    if consts:
        for family in consts:
            _create_csharp_file(family, consts[family])


if __name__ == "__main__":
    main()