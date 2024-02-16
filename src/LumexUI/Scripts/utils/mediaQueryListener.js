/**
 * Copyright (c) 2023 LumexUI.
 * LumexUI licenses this file to you under the MIT license.
 * See the license here https://github.com/LumexUI/lumex-ui/blob/main/LICENSE
 */

/**
 * 
 */
let _mediaQueryList = {};
let _dotNetRef = {};

/**
 * Creates a media query listener using the provided query string.
 * 
 * @param {any} query The media query string to be evaluated.
 * @param {any} selfReference A reference to the .NET object that will be used to invoke the specified method.
 */
function matchMedia(query, dotNetRef) {
    _dotNetRef = dotNetRef;
    _mediaQueryList = window.matchMedia(query);

    _updateMatch(_mediaQueryList);

    _mediaQueryList.addEventListener("change", _updateMatch);
}

/**
 * Detaches the `change` event listener from the media query list.
 */
function destroy() {
    _mediaQueryList.removeEventListener('change', _updateMatch);
}

function _updateMatch(e) {
    _dotNetRef.invokeMethodAsync("MediaQueryChanged", e.matches);
}

export const mediaQueryListener = {
    matchMedia,
    destroy
}