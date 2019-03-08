import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public customers: Customer[];
  public newCustomerName: string;

  constructor(private readonly http: HttpClient) {
    this.loadCustomers();
  }

  private loadCustomers() {
      this.http.get<GetCustomersResult>(environment.apiEndpoint + '/api/Customers/GetCustomers').subscribe(result => {
      this.customers = result.customers;
    }, error => console.error(error));
  }

  public addCustomer() {
    this.http.post(environment.apiEndpoint + '/api/Customers/CreateCustomer', { name: this.newCustomerName }).subscribe(() => {
      this.loadCustomers();
    }, error => console.error(error));
  }
}

interface GetCustomersResult {
    customers: Customer[]
}

interface Customer {
  id: number;
  name: number;
}
