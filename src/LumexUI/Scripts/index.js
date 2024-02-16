/**
 * Copyright (c) 2024 LumexUI.
 * LumexUI licenses this file to you under the MIT license.
 * See the license here https://github.com/LumexUI/lumex-ui/blob/main/LICENSE
 */

import { elementReference } from './utils/elementReference.js'
import { mediaQueryListener } from './utils/mediaQueryListener.js'

export const Lumex = {
    elementReference,
    mediaQueryListener
}

window['Lumex'] = Lumex