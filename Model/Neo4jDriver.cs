using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace playlistimport.Model;

public class Neo4jDriver : IDisposable
{
    private bool _disposed = false; //private variable
    private readonly IDriver _driver;//declaring an IDriver istance

    ~Neo4jDriver() => Dispose(false);//function declaration? pointing to dispose function call

    public Neo4jDriver(string uri, string user, string password)//declaration of constructor?
    {
        _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));//functions from neo4j driver
    }

    public async Task CreateRelationship(string personNodeName, string songNodeTitle)
    {
        var query = @"
        MERGE (p:Person {name: $personNodeName})
        MERGE (s:Song {title: $songNodeTitle})
        MERGE (p)-[:WROTE]->(s)
        RETURN p,s";
    }

    private var session = _driver.AsyncSession();//session receives what asyncsession returns

    EntryPointNotFoundException
    {
        var writeResults = await session.WriteTransactionAsync(async tx =>
        {
            var result = await tx.RunAsync( query, new {personNodeName, songNodeTitle});
            return (await result.ToListAsync());
        });

        foreach (var result in writeResults)//parsing results
        {
            var personNode = result["p"].As<INode>().Properties["name"];//defining node properties
            var songNode = result["s"].As<INode>().Properties["title"];
            Console.WriteLine($"Created Relationship between: {personNode}, {songNode}");
        }
    }
    //Capture any errors along with the query and data for traceability
    catch (Neo4jException ex)
    {
        Console.WriteLine($"{query} - {ex}");
        throw;
    }
    finally
    {
        await session.CloseAsync();
    }
}

public async Task FindPerson(string personName)//defining a function of type Task outside of the original class
{
    var query = @" 
    MATCH (p:Person)
    WHERE p.name = $name
    RETURN p.name";//declaring a variable with the query in t

    var session = _driver.AsyncSession(); //starting a new session with the _driver object.
    try
    {
        //We are reading the results??? From where
        var readResults = await session.ReadTransactionAsync(async tx => 
        {
            var result = await tx.RunAsync(query, new {name = personName});
            return (await result.ToListAsync());
        });

        foreach (var result in readResults)
        {
            Console.WriteLine($"Found person: {result["p.name"].As<String>()}");
        }
    }
    catch (Neo4jException ex)
    {
        Console.WriteLine($"{query} - {ex}");
        throw;
    }
    finally
    {
        await session.CloseAsync();
    }
}

public void Dispose()
{
    Dispose(true);
    GC.SuppressFinalize(this);
}

protected virtual void Dispose(bool disposing)
{
    if (_disposed)
        return;
    if (disposing)
    {
        _driver?.Dispose();
    }

    _dispose = true;
}

public static async Task Main(string[] args)
{
    var uri = "neo4j+s://7068afb4.databases.neo4j.io";

    var user = "neo4j";
    var password = "igp37DmMn_nArtFON1PHYm5xiSQ1gXMBo6kX0vHlOeQ";

    using (var example = new Neo4jDriver(uri, user, password))
    {
        await example.CreateRelationship("Jeremy Loyd", "Down");
        await example.FindPerson("Jeremy Loyd");
    }
}