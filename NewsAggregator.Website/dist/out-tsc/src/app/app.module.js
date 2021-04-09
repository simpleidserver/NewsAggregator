import { __decorate } from "tslib";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '@envs/environment';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DrawerContentService } from './common/matDrawerContent.service';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './infrastructures/auth-guard.service';
import { MaterialModule } from './shared/material.module';
import { appReducer } from './stores/appstate';
import { DatasourceService } from './stores/datasource/services/datasource.service';
import { FeedsEffects } from './stores/feed/effects/feed.effects';
import { FeedService } from './stores/feed/services/feed.service';
export function createTranslateLoader(http) {
    const url = environment.baseUrl + 'assets/i18n/';
    return new TranslateHttpLoader(http, url, '.json');
}
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            HomeComponent
        ],
        imports: [
            BrowserModule,
            AppRoutingModule,
            BrowserAnimationsModule,
            MaterialModule,
            HttpClientModule,
            FlexLayoutModule,
            OAuthModule.forRoot(),
            EffectsModule.forRoot([
                FeedsEffects
            ]),
            StoreModule.forRoot(appReducer),
            TranslateModule.forRoot({
                loader: {
                    provide: TranslateLoader,
                    useFactory: (createTranslateLoader),
                    deps: [HttpClient]
                }
            }),
            StoreDevtoolsModule.instrument({
                maxAge: 10
            })
        ],
        providers: [
            FeedService,
            DatasourceService,
            DrawerContentService,
            AuthGuard
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map