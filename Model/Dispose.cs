namespace playlistimport.Model;

public class Dispose
{
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
}