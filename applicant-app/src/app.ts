require("bootstrap/dist/css/bootstrap.min.css");
require("./styles.css");

import {inject, PLATFORM} from 'aurelia-framework';
import {I18N} from 'aurelia-i18n';
import {RouterConfiguration, Router} from 'aurelia-router';

@inject(I18N)
export class App {
  private router: Router;
  public i18n: I18N;

  constructor(i18n: I18N) {
    this.i18n = i18n;
  }

  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    
    config.title = "Applicant app";
    config.options.pushState = true;
    config.options.root = '/';
    
    config.map([
      { route: ["/"], name: "applicant-form", moduleId: PLATFORM.moduleName("applicant-form") },
      { route: ["/#success"], name: "success", moduleId: PLATFORM.moduleName("success") }
    ]);
  }
}
