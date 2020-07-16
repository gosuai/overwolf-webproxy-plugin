# Overwolf web proxy plugin

This Overwolf plugin allows to start a local webserver that sends CORS headers to client. It's useful for case when you need to pass some data (ex: authentication token) from your website to Overwolf app.

Usage example:
```javascript
// Start web proxy
const webproxy = await new Promise((resolve, reject) => {
  overwolf.extensions.current.getExtraObject('ai.gosu.webproxy', (e) => {
      const webproxy = e.object;
      webproxy.OnRequest.addListener((body, headers, queryparams) => { console.log(body, JSON.parse(headers), JSON.parse(queryparams)) });
      webproxy.Start('http://localhost:9999', '*', '*', '*');
      resolve(webproxy);
  })
})

// Try to request
await fetch('http://localhost:9999?asd=zxc', {method: 'POST', body: JSON.stringify({qwe: 123})})

// And stop web proxy
webproxy.Stop()
```

PRs are welcome, because I'm not a C# developer, so code may be a bit spooky.
