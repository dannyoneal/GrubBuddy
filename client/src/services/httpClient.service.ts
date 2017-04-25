import { Injectable }     from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { AuthService } from "../services/auth.service";
import {Observable} from 'rxjs/Rx';
import {Grub} from '../types/Grub';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class HttpClientService {
    private baseGrubsUrl = "http://localhost:50095/grubs"; //TODO: Need to implement different environments
    grubs: any;
    constructor(private http: Http, private authService: AuthService) {
        this.grubs = [];
    }

    get(url, requestOptions) : Observable<any[]> {
        requestOptions.headers = {"x-grubbuddy-userId": this.authService.getUser().user_id}
        var data = this.http.get(url, requestOptions)
        .map((res:Response) => res.json())
        .retry(1)
        .catch((error:any) => {return []});

        return data;
    }

    post(url, body, requestOptions) : Observable<any[]> {
        return this.http.post(url, body, requestOptions)
        .map((res:Response) => res.json())
        .catch((error:any) => {return []});
    }
}