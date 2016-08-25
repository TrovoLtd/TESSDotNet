using System;
using System.Collections.Generic;

using TrovoSiteSearch.Interfaces;

namespace TrovoSiteSearch.MockSearch
{
    public class MockResultPageAdaptor
    {
        private MockObjects.ResultPage _results;

        public bool RetainProviderFormatting { get; set; }
        public int NumberOfResultsPerPage { get; set; }
        public int PageNumberRequested { get; set; }

        public MockResultPageAdaptor(MockObjects.ResultPage resultList, bool retainProviderFormatting)
        {
            _results = resultList;
            RetainProviderFormatting = retainProviderFormatting;
        }

        public MockResultPage CreateResultPage()
        {
            MockResultPage mockResultPage = new MockResultPage();
            mockResultPage.TotalNumberOfResults = _results.Result.Length;
            mockResultPage.MaxNumberOfResultsPerPage = NumberOfResultsPerPage;
            mockResultPage.CurrentPageNumber = PageNumberRequested;

            if (NumberOfResultsPerPage == 0) NumberOfResultsPerPage = _results.Result.Length;
            if (PageNumberRequested == 0) PageNumberRequested = 1;

            int startPoint = (PageNumberRequested - 1) * NumberOfResultsPerPage;

            int uppermostResultToReturn = 0;
            if ((NumberOfResultsPerPage * PageNumberRequested) < _results.Result.Length)
            {
                uppermostResultToReturn = NumberOfResultsPerPage * PageNumberRequested;
            }
            else
            {
                uppermostResultToReturn = _results.Result.Length;
            }

            if(_results.Result.Length > 0)
            {
                for (int i = startPoint; i < uppermostResultToReturn; i++)
                {
                    MockResultAdaptor adaptor = new MockResultAdaptor(_results.Result[i]);
                    adaptor.RetainProviderFormatting = this.RetainProviderFormatting;
                    mockResultPage.Results.Add(adaptor);
                }
            }

            mockResultPage.SpellingSuggestions = _handleSpellingSuggestions();

            mockResultPage.PromotedLinks = _handlePromotions();

            
            return mockResultPage;
        }

        private List<ITrovoPromotedLink> _handlePromotions()
        {
            List<ITrovoPromotedLink> promotions = new List<ITrovoPromotedLink>();
            if (_results.Promotion != null)
            {
                foreach (MockObjects.Promotion promotion in _results.Promotion)
                {
                    MockPromotedLinkAdaptor promotionAdaptor = new MockPromotedLinkAdaptor(promotion);
                    promotionAdaptor.RetainProviderFormatting = true;
                    promotions.Add(promotionAdaptor);
                }
            }
            return promotions;
        }

        private List<ITrovoSpellingSuggestion> _handleSpellingSuggestions()
        {
            List<ITrovoSpellingSuggestion> spellingSuggestions = new List<ITrovoSpellingSuggestion>();

            if (_results.Spelling != null)
            {
                foreach (MockObjects.Suggestion suggestion in _results.Spelling.Suggestion)
                {
                    MockSpellingSuggestionAdaptor spellingAdaptor = new MockSpellingSuggestionAdaptor(suggestion);
                    spellingAdaptor.RetainProviderFormatting = this.RetainProviderFormatting;
                    spellingSuggestions.Add(spellingAdaptor);
                }
            }
            return spellingSuggestions;
        }


    }
}
