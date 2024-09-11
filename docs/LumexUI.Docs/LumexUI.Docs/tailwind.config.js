const fs = require('fs');
const os = require('os');
const path = require('path');

const nuget = path.join(os.homedir(), '.nuget', 'packages', 'lumexui');
const version = fs.readdirSync(nuget).sort().at(-1);

const lumexui = require(path.join(nuget, version, 'theme', 'plugin'));

/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.razor',
        './Pages/**/*.razor.cs',
        './Components/**/*.razor',
        './Components/**/*.razor.cs',
        '../LumexUI.Docs.Client/Components/**/*.razor',
        '../LumexUI.Docs.Client/Components/**/*.razor.cs',
        `${nuget}/${version}/theme/components/*.cs`
    ],
    darkMode: 'class',
    theme: {
        extend: {
            typography: (theme) => ({
                DEFAULT: {
                    css: {
                        maxWidth: 'none',
                        'h2, h3, h4': {
                            'scroll-margin-top': 'var(--scroll-mt)',
                        },
                        a: {
                            color: 'var(--tw-prose-links) !important',
                            lineHeight: theme('lineHeight.tight'),
                            fontWeight: theme('fontWeight.semibold'),
                            textDecoration: 'none',
                            borderBottom: `1px solid ${theme('colors.orange.400')}`,
                        },
                        'a:hover': {
                            borderBottomWidth: '2px',
                        },
                        'h2 > a': {
                            border: 'none'
                        },
                        pre: {
                            color: theme('colors.zinc.100'),
                            padding: theme('padding.5'),
                        },
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
        },
    ],
};
