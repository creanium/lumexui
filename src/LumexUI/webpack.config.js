const banner = `Copyright (c) LumexUI 2024
LumexUI licenses this file to you under the MIT license
See the license here https://github.com/LumexUI/lumex-ui/blob/main/LICENSE`;

const path = require("path");
const webpack = require("webpack");
const TerserPlugin = require("terser-webpack-plugin");

module.exports = {
    entry: "./Scripts/index.js",
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "LumexUI.min.js"
    },
    plugins: [
        new webpack.BannerPlugin(banner),
    ],
    optimization: {
        minimize: true,
        minimizer: [new TerserPlugin({
            extractComments: false
        })]
    }
};