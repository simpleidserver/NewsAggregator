import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import * as fromAppState from '@app/stores/appstate';
import { Datasource } from '@app/stores/datasource/models/datasource.model';
import * as fromFeedActions from '@app/stores/feed/actions/feed.actions';
import { Feed } from '@app/stores/feed/models/feed.model';
import { SearchFeedsResult } from '@app/stores/feed/models/search-feed.model';
import { select, Store } from '@ngrx/store';
import { AddFeedDialog } from './add-feed.component';

@Component({
  selector: 'app-feed-list',
  templateUrl: './feed-list.component.html',
  styleUrls: ['./feed-list.component.sass']
})
export class FeedListComponent implements OnInit, OnDestroy {
  listener: any;
  displayedColumns: string[] = ['checkbox', 'feedTitle', 'datasourceTitle', 'nbFollowers', 'nbStoriesPerMonth'];
  feeds: Feed[] = [];
  feedsToBeRemoved: Feed[] = [];
  feedTitle: string;
  datasource: string;
  selectedFollower: string;
  selectedStory: string;
  datasources: Datasource[];

  constructor(
    private dialog: MatDialog,
    private store: Store<fromAppState.AppState>) { }

  ngOnInit(): void {
    this.refresh();
    this.listener = this.store.pipe(select(fromAppState.selectFeedSearchResult)).subscribe((r: SearchFeedsResult | null) => {
      if (!r) {
        return;
      }

      this.feeds = r.content;
    });
  }

  ngOnDestroy(): void {
    if (this.listener) {
      this.listener.unsubscribe();
    }
  }

  addFeed() {
    const dialogRef = this.dialog.open(AddFeedDialog);
    dialogRef.afterClosed().subscribe(result => {
      const request = fromFeedActions.startAddFeed({ feedTitle: result.feedTitle, datasource: result.datasource });
      this.store.dispatch(request);
    });
  }

  deleteFeed(feed: Feed) {
    const filtered = this.feedsToBeRemoved.filter((f: Feed) => f.datasourceId === feed.datasourceId && f.feedId === feed.feedId);
    if (filtered.length === 1) {
      this.feedsToBeRemoved = this.feedsToBeRemoved.filter((f: Feed) => f.datasourceId !== feed.datasourceId || f.feedId !== feed.feedId);
      return;
    }

    this.feedsToBeRemoved.push(feed);
  }

  remove() {
    if (this.feedsToBeRemoved.length === 0) {
      return;
    }

    const request = fromFeedActions.startDeleteDatasources({ parameters: this.feedsToBeRemoved.map((v: Feed) => new fromFeedActions.DeleteDatasource(v.feedId, v.datasourceId)) });
    this.store.dispatch(request);
  }

  refresh() {
    const startIndex: number = 0;
    const count: number = 100;
    const request = fromFeedActions.startSearchFeeds({ order: 'createDateTime', direction: 'desc', count: count, startIndex: startIndex, feedTitle: this.feedTitle });
    this.store.dispatch(request);
  }
}