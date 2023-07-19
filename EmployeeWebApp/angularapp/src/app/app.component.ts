import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'angularapp';
  public projects?: EmployeeProject[];

  constructor(private http: HttpClient) {
    
  }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.http.get<EmployeeProject[]>('api/employeeproject').subscribe(result => {
      this.projects = result;
      console.log(this.projects);
    }, error => console.error(error));
  }

  onFileSelected(event: Event) {
    const element = event.currentTarget as HTMLInputElement;
    let fileList: FileList | null = element.files;
    if (fileList && fileList.length > 0) {
      this.projects = [];

      const formData = new FormData();
      formData.append("file", fileList[0]);
      
      const upload$ = this.http.post("api/employeeproject", formData);
      upload$.subscribe(result => {
        this.loadData();
      }, error => console.error(error));
    }
  }
}

interface EmployeeProject {
  employee1Id: number;
  employee2Id: number;
  projectId: number;
  days: number;
}