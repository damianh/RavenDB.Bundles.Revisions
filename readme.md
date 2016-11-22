RavenDB.Bundles.Revisions
=========================

A RavenDB plugin and client that automatically maintains previous versions of a document, if the document has a Revision property of type int. This is an alternative to Raven's 'Versioning' bundle in that:

* It only works on documents that have a Revision property.
* It allows Revisions to be deleted.
* It allows Revisions to be overwritten.
* Your application controls the Revision number and not the plugin.
* Your application is resposible for revision number contiguity.

While Raven's 'Versioning' bundle is designed for regulated environments, such as healtcare, where nothing can be deleted, this bundle is more useful for applications where the document is a result of a projection (i.e. CQRS) and where the 'source of truth' comes from somewhere else and where projections / documents can easily be rebuilt.



### Install

Packages are available nuget.org

* https://www.nuget.org/packages/RavenDB.Bundles.Revisions - use this with the embedded database or extract and xcopy deploy to server plugins directory
* https://www.nuget.org/packages/RavenDB.Client.Revisions - extends the RavenDB.Client.Lightweight to allow retrieving of specfic revisions of a (revisionable) document

### Using

If you want your this plugin to automatically create revision copies add a Revision property to your document:

<pre>
public class MyDocument
{
    public string Id { get; set;}
    
    public int Revision { get; set; }
}
</pre>

When a revisionable document is saved, the plugin will create a _copy_ with a new id. These copies are excluded from all indexes. You should be aware that these revisions are kept indefinitely until you have specifically deleted them or used Raven's 'Expiration' bundle to perform automatic purging.

To retireve a specific revision of a document use the LoadRevision extension method:

<pre>
using(var session = store.OpenSession)
{
    var doc = session.LoadRevision<MyDocument>("key", 2);
}
</pre>

To delete a specific revision of a document:

<pre>
_documentStore.DatabaseCommands.DeleteRevision("key", 2, null);
<ptr>

