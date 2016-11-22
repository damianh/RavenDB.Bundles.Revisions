#region CopyrightAndLicence

// --------------------------------------------------------------------------------------------------------------------
// <Copyright company="Damian Hickey" file="AsyncDatabaseCommandsExtensions.cs">
// 	Copyright © 2012 Damian Hickey
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

namespace Raven.Client.Revisions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Raven.Client.Connection.Async;

    public static class AsyncDatabaseCommandsExtensions
    {
        public static Task DeleteRevision(this IAsyncDatabaseCommands databaseCommands,
            string id,
            int revision,
            Guid? etag = default(Guid?),
            CancellationToken ct = default(CancellationToken))
        {
            if (databaseCommands == null) throw new ArgumentNullException(nameof(databaseCommands));
            if (id == null) throw new ArgumentNullException(nameof(id));

            var revisionDocId = RevisionDocIdGenerator.GetId(id, revision);

            return databaseCommands.DeleteAsync(revisionDocId, etag, ct);
        }
    }
}