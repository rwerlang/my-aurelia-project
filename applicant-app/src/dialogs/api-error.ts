import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class ApiErrorDialog {
    public controller: DialogController;
    public model: any;

    constructor(controller: DialogController) {
        this.controller = controller;

        controller.settings.lock = false;
        controller.settings.centerHorizontalOnly = true;
    }

    activate(model: any) {
        this.model = model;
    }
}
