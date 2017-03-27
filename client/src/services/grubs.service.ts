import { Injectable }     from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import {Observable} from 'rxjs/Rx';
import {Grub} from '../types/Grub';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class GrubsService {
    private baseGrubsUrl = "http://localhost:50095/grubs";
    grubs: any;
    constructor(private http: Http) {
        this.grubs = [];
    }


    getGrubs() : Observable<Grub[]> {
        var action = "/get";
        this.grubs = this.http.get(this.baseGrubsUrl + action)
        .map((res:Response) => res.json())
        .retry(2)
        .catch((error:any) => {return []});

        return this.grubs;
    }

    searchGrubs(searchText:string) {
        var action = "/getByName/" + searchText;
        return this.http.get(this.baseGrubsUrl + action)
        .map((res:Response) => res.json())
        .retry(2)
        .catch((error:any) => Observable.throw(error.json().error || 'Server error'));
    }
}