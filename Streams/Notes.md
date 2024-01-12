# Stream
A .NET class, relating to serial read/write operations. Provides a generic view of different IO types and abstracts away complexities of the operating system and underlying devices.

>Serial access is where data can only be accessed in the order that it was written

The `Stream` class, provided in the `System.IO` namespace exposes via methods to read from, and/or write to this backing store, serially - either one byte at a time, or in manageable chunks. This means a stream can use a small fixed amount of memory, regardless of the size of the backing store.

## Architecture
Consists of 3 concepts:
- Backing store
- Decorators
- Adapters

![stream architecture](https://www.oreilly.com/api/v2/epubs/9781449334192/files/httpatomoreillycomsourceoreillyimages1233194.png)

### Backing store
A low-level source from which bytes can be sequentially read, or a destination to which bytes can be sequentially written, **or both**. A backing store stream is a hard-wired stream to a specific type of backing store, e.g. `FileStream` or `NetworkStream`

Deals exclusively with bytes.

### Decorators
Optional decorator stream to be added, feeding on another stream. Sits between the adapter and the backing store stream. Adds functionality such as:
- encryption
- compression

Deals exclusively with bytes.

### Adapters
Stream adapters are what we're more familiar with. These are the exposed methods to deal with higher-level types like strings or xml. For example, .NET provides a `StreamReader` and `StreamWriter` class for dealing with strings, and also an `XmlReader` and `XmlWriter` class for dealing with xml.

## FileStream and MemoryStream
Both these classes inherit from `Stream`. You will choose one over the other depending on what your backing store is - for example is it a file on disk you're trying to access, or is it something held in memory? There are other abstractions of `Stream` but these are the most common.

The `StreamReader` is a helper class to provide methods to read a `string` from a `FileStream` or a `MemoryStream` - which remember, just read *bytes*.

![stream-reader](https://www.tutorialsteacher.com/Content/images/csharp/stream-relations.png) 