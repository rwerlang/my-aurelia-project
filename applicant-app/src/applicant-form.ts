import {inject,NewInstance} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {ValidationRules, ValidationController} from 'aurelia-validation';
import {I18N} from 'aurelia-i18n';
import {DialogService} from 'aurelia-dialog';
import {Endpoint, Rest} from 'aurelia-api';
import {Router} from 'aurelia-router';

import {BootstrapFormRenderer} from './helpers/bootstrap-form-renderer';
import { ApplicantInputModel } from './applicant-input-model';
import { PromptYesNo  } from './dialogs/yesno';
import { ApiErrorDialog } from './dialogs/api-error';

@inject(
    NewInstance.of(ValidationController), 
    I18N, 
    DialogService,
    Endpoint.of('api'),
    Router)
export class ApplicantForm {
    private router: Router;
    private api: Rest;
    private dialogService: DialogService;

    public model: ApplicantInputModel; 
    public controller: ValidationController;
    public i18n: I18N;
    public errors: Array<any>;

    public get isValid(): boolean {
        return this.errors.length == 0
            && this.model
            && this.model.name.length > 0
            && this.model.familyName.length > 0
            && this.model.address.length > 0
            && this.model.age
            && this.model.countryOforigin.length > 0
            && this.model.emailAddress.length > 0;
    };

    constructor(
        controller: ValidationController,
        i18n: I18N,
        dialogService: DialogService,
        api: Rest,
        router: Router) {

        this.router = router;
        this.i18n = i18n;
        this.errors = [];
        this.controller = controller;
        this.controller.addRenderer(new BootstrapFormRenderer());

        this.api = api;
        this.dialogService = dialogService;
        
        this.initializeForm();
        this.configureValidationRules();
    }

    configureValidationRules() : void {
        ValidationRules
            .ensure((a: ApplicantInputModel) => a.name)
            .displayName(this.i18n.tr("name"))
            .required()
            .minLength(5)
            .maxLength(100)

            .ensure((a: ApplicantInputModel) => a.familyName)
            .displayName(this.i18n.tr("familyName"))
            .required()
            .minLength(5)
            .maxLength(100)

            .ensure((a: ApplicantInputModel) => a.address)
            .displayName(this.i18n.tr("address"))
            .required()
            .minLength(10)
            .maxLength(200)

            .ensure((a: ApplicantInputModel) => a.countryOforigin)
            .displayName(this.i18n.tr("countryOforigin"))            
            .required()
            .maxLength(100)

            .ensure((a: ApplicantInputModel) => a.emailAddress)
            .displayName(this.i18n.tr("email"))
            .required()
            .email()
            .maxLength(100)

            .ensure((a: ApplicantInputModel) => a.age)
            .displayName(this.i18n.tr("age"))
            .required()
            .between(19, 61)

            .on(this.model);
    }

    addApplicant(): void {
        this.controller.validate().then(result => {
            if (result.valid) {
                this.logToConsole(this.model);
                
                this.api.post("applicants", this.model).then(resp => {
                    this.logToConsole(resp);
                    this.router.navigate("/#success");
                })
                .catch(err => {
                    err.json().then((data: any) => {
                        this.logToConsole(data);
                    
                        this.dialogService.open({
                            viewModel: ApiErrorDialog,
                            model: data
                        });    
                    });
                });
            }
        });
    }

    initializeForm(): void {
        this.model = {
            name: "",
            familyName: "",
            address: "",
            age: null,
            countryOforigin: "",
            emailAddress: "",
            hired: false
        };
    }

    reset(): void {
        this.dialogService.open({ 
            viewModel: PromptYesNo, 
            model: this.i18n.tr("resetQuestion")
        }).whenClosed(response => {
            if (!response.wasCancelled) {
                this.initializeForm();
            }
        });
    }

    private logToConsole(data: any): void {
        if (environment.debug) {
            console.log(data);
        }
    }
}
