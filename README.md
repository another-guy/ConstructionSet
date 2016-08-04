## Synopsis

Simplifies C# reflection code.

## Status
| | |
| --- | --- |
| NuGet (stable) | [![NuGet](https://img.shields.io/nuget/v/Mirror.svg)](https://www.nuget.org/packages/Mirror/) [![NuGet](https://img.shields.io/nuget/vpre/Mirror.svg)](https://www.nuget.org/packages/Mirror/) |
| MyGet (latest) | [![MyGet CI](https://img.shields.io/myget/another-guy/v/Mirror.svg)](https://www.myget.org/feed/another-guy/package/nuget/Mirror) [![MyGet CI](https://img.shields.io/myget/another-guy/vpre/Mirror.svg)](https://www.myget.org/feed/another-guy/package/nuget/Mirror) |
| Build| [![Build status](https://ci.appveyor.com/api/projects/status/as29kthpwxftaiy6?svg=true)](https://ci.appveyor.com/project/another-guy/mirror) |
| Issues and pull requests | [![GitHub issues](https://img.shields.io/github/issues/another-guy/mirror.svg?maxAge=2592000)](https://github.com/another-guy/Mirror/issues) |

## Code Example

### Describing the target

To access or invoke a private method that is defined on a `type` or an `object` client code needs to specify the target correctly.
Targets' members can be either static or instance.
Notice that `constructors` must be accessed through a **static** target since no **instance target** is available yet:

```cs
class MyClass {
  private string name;
  private MyClass() { this.name = "Name is not set"; }
  private MyClass(string name) { this.name = name; }
}

MyClass targetObject = ...;

var staticTarget = Use.Target<MyClass>();
// staticTarget can now be used to call constructor or
// call or access static members

var instanceTarget = Use.Target(targetObject)
// instanceTarget can now be used to call or access targetObject's members
```

### Creating an instance of an object

```cs
// Creates object by calling its default constructor
MyClass newInstance = Use.Target<MyClass>().ToCreateInstance();

// Creates object by calling its best matching parameterized constructor
MyClass newInstance = Use.Target<MyClass>().ToCreateInstance("Alice");
```

### Working with instance members

```cs
class MyClass {
  private string name;
  private string Name { get { return name; } set { name = value } }
  private void SetName(string newName) { name = newName; }
  private string GetName() { return name; }
}
MyClass target = new MyClass();

// Field/Property access code looks similar
Use.Target(target).ToSet("name").Value("Bob")
Use.Target(target).ToSet("Name").Value("Chris")
string nameFromField = Use.Target(target).ToGet<string>("name");
string nameFromProperty = Use.Target(target).ToGet<string>("Name");

// Method access
Use.Target(target).ToCall("SetName", "David");
string nameFromMethod = Use.Target(target).ToCall<string>("GetName");
```

### Working with static members

```cs
static class MyClass {
  private static string name;
  private static string Name { get { return name; } set { name = value } }
  private static void SetName(string newName) { name = newName; }
  private static string GetName() { return name; }
}

Use.Target<MyClass>().ToSet("name").Value("Bob")
string nameFromStaticField = Use.Target<MyClass>().ToGet<string>("name");

Use.Target<MyClass>().ToSet("Name").Value("Chris")
string nameFromStaticProperty = Use.Target<MyClass>().ToGet<string>("Name");

Use.Target<MyClass>().ToCall("SetName", "David");
string nameFromStaticMethod = Use.Target<MyClass>().ToCall<string>("GetName");
```

## Motivation

Syntax sugar is syntax sugar: it's not a necessary thing per se but it can improve code quality.
Reflection should be your last resort tool when used in production code:
types very often limit constructor and other members' accessibility level on purpose.

In your tests, however, it may be okay to have slightly sloppier code.
For example, you may want to build a dummy/fake object and for some reason mocking tools like NSubstitute can't do it.

## Installation

Mirror is a available in a form of a NuGet package.
Follow regular installation process to bring it to your project.
https://www.nuget.org/packages/Mirror/

## Tests

Unit tests are available in Mirror.Tests project.

## License

The code is distributed under the MIT license.

## Contributing

Contribution is the best way to improve any project!

1. Fork it!
2. Create your feature branch (```git checkout -b my-new-feature```).
3. Commit your changes (```git commit -am 'Added some feature'```)
4. Push to the branch (```git push origin my-new-feature```)
5. Create new Pull Request

...or follow steps described in a nice [fork guide](http://kbroman.org/github_tutorial/pages/fork.html) by Karl Broman
