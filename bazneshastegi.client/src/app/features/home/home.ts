import {Component, OnInit} from '@angular/core';
import {RestApiService} from '../../core/rest-api.service';
import {RequestType, RequestTypeResponse} from '../../core/models/RequestTypeResponse';
import {MatSelectChange} from '@angular/material/select';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home implements OnInit {
  requestTypes: RequestType[] = [];
  message: string = '';

  constructor(private restApiService: RestApiService,
              private router: Router) {
  }

  ngOnInit() {
    this.restApiService.getRequestTypes().subscribe((a: RequestTypeResponse) => {
      this.requestTypes = a.data;
    });
    const requestNo = sessionStorage.getItem('modify-person-info');
    if (requestNo) {
      this.message = `متقاضی گرامی درخواست شما با شماره پیگیری ${requestNo} در سامانه ثبت گردید. جهت مشاهده مراحل بررسی درخواست از طریق منوی پیگیری درخواست اقدام فرمایید.`;
      sessionStorage.removeItem('modify-person-info');
    }
  }

  selectForm($event: MatSelectChange<RequestType>) {
    const requestType = $event.value;
    const url = `/forms/${requestType.page.toLowerCase()}/${requestType.requestTypeID}`;
    this.router.navigate([url]).then(() => {
    });
  }
}
