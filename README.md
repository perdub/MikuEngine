# MikuEngine

This library is a simple engine to visual novels. You can add it in any game engine(who supports csharp) and create `Engine` class, something like this:
```
Engine e = new Engine((text)=>{Console.WriteLine(text);}, ()=>{Console.ReadKey})
```
And invoke `e.ExecuteScript("print(\"1\");wait();print(\"2\")")`

## JavaScript

If really, MikuEngine use a [Jint](https://github.com/sebastienros/jint), so you`re a just execute js code.