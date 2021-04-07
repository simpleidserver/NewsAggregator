import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@app/shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { FeedRoutes } from './feed.routes';
import { AddFeedDialog } from './list/add-feed.component';
import { FeedListComponent } from './list/feed-list.component';
import { FeedViewComponent } from './view/view-feed.component';
import { DatasourceSelectorComponent } from '../common/datasourceselector/datasourceselector.component';

@NgModule({
  declarations: [
    AddFeedDialog,
    FeedListComponent,
    FeedViewComponent,
    DatasourceSelectorComponent
  ],
  entryComponents: [
    AddFeedDialog
  ],
  imports: [
    FeedRoutes,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class FeedModule { }
