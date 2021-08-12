using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ChallengeSuperLiga.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ClientIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        }
    }
}