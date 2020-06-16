# ParallelTcpClientConnectionApp
* The app is example of that dynamic defined tcp clients connect via parallel.
* First of all, you should enter the **ip** and **port** information on the screen where you want to establish a socket connection.
* Then, by clicking the **Connect** button, it will try to connect simultaneously to the defined ip-port end connections.
* In each line, the related ip-port connection status will be displayed as a picture and description.
* You must click the **Disconnect** button to close all connections.
* For successful connections, the **Send Data** button will be active.
* You can send data to the terminals with active connection by clicking the **Send Data** button.
* A ping-pong-like structure has been created to check whether the connection is broken or not in the application. For this, the TcpClientEx class was derived from the TcpClien class and the Connect, Close and Dispose methods were overrrided. In the Connect method, the timer of the instance is started and checks the connection status by checking the ping-pong tests at regular intervals. If the connection is broken, it throws the OnDisconnect event.

 ```csharp
 public TcpClientEx(int rowIndex):base()
  {
      this.rowIndex = rowIndex;            
      tmr = new System.Timers.Timer(TimeSpan.FromSeconds(15).TotalMilliseconds);
      tmr.Elapsed += Tmr_Elapsed;            
  }
  ```
   ```csharp
  public new async Task ConnectAsync(string hostname, int port)
  {
      this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
      await base.ConnectAsync(hostname, port);
      this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
      tmr.Start();            
  }
   ```
   ```csharp
  public new void Close()
  {
      base.Close();
      tmr.Stop();
  }

  public new void Dispose()
  {
      base.Dispose();
      tmr.Dispose();
  }
 ```
 ```csharp
  private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
  {
      if(!this.Client.IsConnected())
      {
          Console.WriteLine("Tmr_Elapsed called!...(Disconnected)");

          if (OnDisconnect != null)
              OnDisconnect(this, new OnDisconnectEventArgs(this.rowIndex));
      }
      else
      {
          Console.WriteLine("Tmr_Elapsed called!...(Connected)");
      }


  }
 ```

## Screenshots
![app-screenshot-03](https://user-images.githubusercontent.com/42136540/84788192-89afbc80-aff7-11ea-80a9-ee9957e27fec.PNG)
![app-screenshot-01](https://user-images.githubusercontent.com/42136540/84787653-ec548880-aff6-11ea-86e3-d4384db3238a.PNG)
![app-screenshot-02](https://user-images.githubusercontent.com/42136540/84787700-fc6c6800-aff6-11ea-8254-ad03870b0dbf.PNG)
