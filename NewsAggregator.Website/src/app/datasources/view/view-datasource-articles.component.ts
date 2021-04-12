import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as fromAppState from '@app/stores/appstate';
import * as fromArticleActions from '@app/stores/articles/actions/article.actions';
import { Store } from '@ngrx/store';
import { DrawerContentService } from '../../common/matDrawerContent.service';
import { ViewArticlesComponent } from '../../common/viewArticles/viewArticles.component';

@Component({
  selector: 'app-datasource-view-articles',
  templateUrl: '../../common/viewArticles/viewArticles.component.html',
  styleUrls: ['../../common/viewArticles/viewArticles.component.sass']
})
export class DatasourceViewArticlesComponent extends ViewArticlesComponent {
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected store: Store<fromAppState.AppState>,
    protected drawerContentService: DrawerContentService) {
    super(activatedRoute, store, drawerContentService);
  }

  refresh(startIndex : number) {
    const datasourceId = this.activatedRoute.snapshot.params['id'];
    const request = fromArticleActions.startSearchArticlesInDatasource({ count: this.count, startIndex: startIndex, order: 'createDateTime', direction: 'desc', datasourceId: datasourceId });
    this.store.dispatch(request);
  }
}
