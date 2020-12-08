import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http"
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  //post employee
  postEmployeeRecord(formData) {
    return this.http.post(environment.apiBaseURI +"/Employee/Add", formData);
  }

  //patch employee
  patchEmployeeRecord(formData) {
    return this.http.patch(environment.apiBaseURI + "/Employee/Update", formData);
  }

  //delete the employee
  deleteEmployeeRecord(id) {
    return this.http.delete(environment.apiBaseURI + "/Employee/Delete?id=" + id); 
  }

  //get the employees
  getEmployeeRecords() {
    return this.http.get(environment.apiBaseURI + "/Employee/Get");
  }
}
