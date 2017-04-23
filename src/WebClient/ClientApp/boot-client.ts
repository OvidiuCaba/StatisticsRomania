// TODO: run this manually to generate vendor files: webpack --config webpack.config.vendor.js
// TODO: automate the above step; it should be generated at runtime

import 'angular2-universal-polyfills/browser';
import { enableProdMode } from '@angular/core';
import { platformUniversalDynamic } from 'angular2-universal';
import { AppModule } from './app/app.module';
//import 'bootstrap';   // commented because it breaks some of the NGB [Angular Bootstrap] controls [eg: group buttons]; however, bootstrap.js should not be used together with ng-bootstrap: https://github.com/ng-bootstrap/ng-bootstrap/issues/1101
const rootElemTagName = 'app'; // Update this if you change your root component selector

// Enable either Hot Module Reloading or production mode
if (module['hot']) {
    module['hot'].accept();
    module['hot'].dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector(rootElemTagName);
        const newRootElem = document.createElement(rootElemTagName);
        oldRootElem.parentNode.insertBefore(newRootElem, oldRootElem);
        platform.destroy();
    });
} else {
    enableProdMode();
}

// Boot the application, either now or when the DOM content is loaded
const platform = platformUniversalDynamic();
const bootApplication = () => { platform.bootstrapModule(AppModule); };
if (document.readyState === 'complete') {
    bootApplication();
} else {
    document.addEventListener('DOMContentLoaded', bootApplication);
}
