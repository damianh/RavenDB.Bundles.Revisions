#region CopyrightAndLicence

// --------------------------------------------------------------------------------------------------------------------
// <Copyright company="Damian Hickey" file="RevisionDocumentPutTrigger.cs">
// 	Copyright � 2012 Damian Hickey
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </Copyright>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Raven.Bundles.Revisions
{
    using Raven.Abstractions.Data;
    using Raven.Database.Plugins;
    using Raven.Json.Linq;

    public class HideRevisionDocumentsFromIndexingReadTrigger : AbstractReadTrigger
    {
        public override ReadVetoResult AllowRead(
            string key,
            RavenJObject metadata,
            ReadOperation operation,
            TransactionInformation transactionInformation)
        {
            return operation != ReadOperation.Index
                ? ReadVetoResult.Allowed
                : (key.Contains(RevisionDocumentPutTrigger.RevisionSegment)
                    ? ReadVetoResult.Ignore
                    : ReadVetoResult.Allowed);
        }
    }
}