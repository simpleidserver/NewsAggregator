﻿namespace NewsAggregator.Core.Parameters
{
    public class SearchRecommendationsParameter
    {
        public string UserId { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
