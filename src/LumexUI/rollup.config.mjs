import { defineConfig } from 'rollup';
import resolve from '@rollup/plugin-node-resolve';

export default defineConfig([
    {
        input: 'wwwroot/js/components/popover.js',
        output: {
            file: 'wwwroot/js/components/popover.bundle.js',
            format: 'esm',
        },
        plugins: [resolve()],
    },
    {
        input: 'Scripts/Plugin/plugin.js',
        output: {
            file: 'Scripts/Plugin/plugin.bundle.js',
            format: 'cjs',
        },
        plugins: [resolve()],
    }
]);
