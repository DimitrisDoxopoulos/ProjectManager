import {Component, inject, Inject, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSidenavModule} from "@angular/material/sidenav";
import {map, Observable, shareReplay} from "rxjs";
import {BreakpointObserver, Breakpoints} from "@angular/cdk/layout";
import {MatToolbarModule} from "@angular/material/toolbar";
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatExpansionModule} from "@angular/material/expansion";
import {LoginComponent} from "../../components/login/login.component";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, MatSidenavModule, MatToolbarModule, RouterLink, MatButtonModule, MatIconModule, MatListModule, MatExpansionModule, RouterOutlet],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private authService: AuthService) {

  }

  logout() {
    this.authService.logout()
  }
}
