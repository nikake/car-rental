import { Component, OnInit } from '@angular/core';
import { MenuItem } from './models/menu-item';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  public menuItems: MenuItem[] = [new MenuItem('Uthyrning', '/'), new MenuItem('Återlämning', '/retur')];
  public currentMenu: number = 0;
  ngOnInit() {

  }
}
