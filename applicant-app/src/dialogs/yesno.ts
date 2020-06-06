import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class PromptYesNo {
    public controller: DialogController;
    public message: string;

    constructor(controller: DialogController) {
        this.controller = controller;

        controller.settings.lock = false;
        controller.settings.centerHorizontalOnly = true;
    }

    activate(message: string) {
        this.message = message;
    }
}
