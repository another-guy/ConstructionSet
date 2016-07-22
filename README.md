## Synopsis

Helps using reflection in C#

## Code Example

An object can be created by calling its private default constructor:

```cs
MyClass result = Create<MyClass>.UsingPrivateConstructor();
```

If the class has a private constructor that accepts parameters, it can be invoked too.
The Create<T> class will do its best to find the constructor with a signature matching types of the passed arguments.

```cs
MyClass result = Create<MyClass>.UsingPrivateConstructor(1, "a");
```

## Motivation

Syntax sugar is syntax sugar: it's not a necessary thing per se but it can improve code quality.
Reflection should be your last resort tool when used in production code:
types very often limit constructor and other members' accessibility level on purpose.

In your tests, however, it may be okay to have slightly sloppier code.
For example, you may want to build a dummy/fake object and for some reason mocking tools like NSubstitute can't do it.

## Installation

ConstructionSet is a available in a form of a NuGet package.
Follow regular installation process to bring it to your project.
https://www.nuget.org/packages/ConstructionSet/

## Tests

Unit tests are available in ConstructionSet.Tests project.

## License

The code is distributed under the MIT license.