import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';

@inject(Router)
export class Success {
    private router: Router;

    constructor(router: Router) {
        this.router = router;
    }

    clickBack(): void {
        this.router.navigate("/");
    }
}