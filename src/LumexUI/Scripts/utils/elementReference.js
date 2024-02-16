/**
 * Copyright (c) 2023 LumexUI.
 * LumexUI licenses this file to you under the MIT license.
 * See the license here https://github.com/LumexUI/lumex-ui/blob/main/LICENSE
 */

/**
 * 
 * @param {any} element
 */
function blur(element) {
    if (!element) {
        throw "No element found!";
    }

    element.blur();
}

/**
 * 
 * @param {any} element
 * @returns
 */
function getScrollHeight(element) {
    if (!element) {
        throw "No element found!";
    }

    return element.scrollHeight;
}

export const elementReference = {
    blur,
    getScrollHeight
}