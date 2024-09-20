//const fs = require('fs');
//const os = require('os');
//const path = require('path');

//const nuget = path.join(os.homedir(), '.nuget', 'packages', 'lumexui');
//const version = fs.readdirSync(nuget).sort().at(-1);

//const lumexui = require(path.join(nuget, version, 'theme', 'plugin'));

const defaultTheme = require('tailwindcss/defaultTheme');
const lumexui = require('../../../src/LumexUI/Scripts/Plugin/dist/plugin');

/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.razor',
        './Pages/**/*.razor.cs',
        './Components/**/*.razor',
        './Components/**/*.razor.cs',
        '../LumexUI.Docs.Client/Pages/**/*.razor',
        '../LumexUI.Docs.Client/Pages/**/*.razor.cs',
        '../LumexUI.Docs.Client/Components/**/*.razor',
        '../LumexUI.Docs.Client/Components/**/*.razor.cs',
        '../../../src/LumexUI/Styles/*.cs'
        /*`${nuget}/${version}/theme/components/*.cs`*/
    ],
    darkMode: 'class',
    theme: {
        extend: {
            fontFamily: {
                sans: ['Inter var', ...defaultTheme.fontFamily.sans],
                mono: ['Fira Code var', ...defaultTheme.fontFamily.mono]
            },
            typography: (theme) => ({
                DEFAULT: {
                    css: {
                        maxWidth: 'none',
                        'h2, h3, h4': {
                            'scroll-margin-top': 'var(--scroll-mt)',
                        },
                        a: {
                            color: 'var(--tw-prose-links)',
                            lineHeight: theme('lineHeight.tight'),
                            fontWeight: theme('fontWeight.semibold'),
                            textDecoration: 'none',
                            borderBottom: `1px solid ${theme('colors.orange.400')}`,
                        },
                        'a:hover': {
                            borderBottomWidth: '2px',
                        },
                        pre: {
                            color: theme('colors.zinc.100'),
                            padding: theme('padding.5'),
                        },
                        'table code::before': {
                            content: '""'
                        },
                        'table code::after': {
                            content: '""'
                        }
                    }
                }
            }),
        },
    },
    plugins: [
        lumexui,
        require('@tailwindcss/typography'),
        function ({ addVariant }) {
            addVariant('children', '& > *')
            addVariant('scrollbar', '&::-webkit-scrollbar')
            addVariant('scrollbar-track', '&::-webkit-scrollbar-track')
            addVariant('scrollbar-thumb', '&::-webkit-scrollbar-thumb')
        },
        function ({ addUtilities, theme }) {
            let color = theme('colors.indigo.200').replace('#', '%23');
            let backgroundImage = `url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' viewBox='0 0 16 16'%3E%3Ccircle cx='8' cy='8' r='1' fill='${color}' /%3E%3C/svg%3E")`;

            addUtilities({
                '.bg-dots': {
                    backgroundImage
                },
            })
        }
    ],
};
