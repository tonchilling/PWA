const path = require('path');
module.exports = {
    entry: {
        'main/bundle': './Scripts/Es6/main.js',
        'main2/bundle': './Scripts/Es6/main2.js',
        'IncidentCaseReceive/bundle': './Scripts/ES6/Views/IncidentCaseReceive.js',
    },
    output: {
        path: path.resolve(__dirname, './Scripts/Build'),
        filename: '[name].js'
    },
    // IMPORTANT NOTE: If you are using Webpack 2 or above, replace "loaders" with "rules"
    module: {
        rules: [{
            loader: 'babel-loader',
            test: /\.js$/,
            exclude: /node_modules/
        }]
    }
}