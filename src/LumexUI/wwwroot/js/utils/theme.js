// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

const props = {
    KEY: "theme",
    LIGHT: "light",
    DARK: "dark",
}

function initialize(defaultTheme) {
    const storedTheme = localStorage.getItem(props.KEY);
    const theme = storedTheme || defaultTheme;

    if (!storedTheme) {
        localStorage.setItem(props.KEY, theme);
    }

    document.documentElement.classList.add(theme);
}

function toggle() {
    const currentTheme = localStorage.getItem(props.KEY);
    const newTheme = currentTheme === props.LIGHT
        ? props.DARK
        : props.LIGHT;

    localStorage.setItem(props.KEY, newTheme);

    document.documentElement.classList.remove(currentTheme);
    document.documentElement.classList.add(newTheme);

    return newTheme === props.DARK;
}

export const theme = {
    initialize,
    toggle  
}