# ArgumentParser

ArgumentParser is a package created with the purpose of making it easier to parse command line arguments without using reflection.
While there are many other packages that parse arguments, I have yet to see an easy to use one that doesn't use reflection.
This feature is crucial when you want to be able to compile your cli's to native code, as we are now able with .NET 7.

## Features

* 🚀 High performance, Low memory usage and allocation
* 🪶 Very lightweight (No external dependencies)
* 🔌 Plug and play (most of the time you don't need to change your code much)
* ✂ Trimming friendly (no reflection uses at all)
* Supports all platforms (Windows, Linux, Mac)

## ⬇ Installation

* The last stable release will be available in the releases section in a .dll format.
* Nuget   [![](https://img.shields.io/nuget/dt/ArgumentParser?label=Downloads)](https://www.nuget.org/packages/ArgumentParser/)

## Usage

`Parser.Split()` is a static method that allows splitting all components of the arguments into a collection of string.
This is different from string.Split() as it is both more efficient, and respects quotes which allows you to put everything
in between quotes in a single argument.

`Parser.ParseArguments()` is a static method that allows parsing the arguments into an `Arguments` object that is a wrapper
over a `Dictionary<string, string>` that allows very convienient access to the arguments.

### Features of the `Arguments` object

* Access to named arguments by name
* Access to positional arguments by index as int e.g: "0" for the first positional argument
* Conversion of arguments to `int`, `double` or `decimal` with validation.
* Validation of any value of an argument with custom `Func<string, bool> validationFunc`
* Access to underlying `Dictionary<string, string>` with `Arguments.GetInnerDictionary()`
* An option to lower the index of all positional arguments by 1 with `Arguments.ForwardPositionalArguments` which means "1" will now become "0"
and what was "0" will be removed, this is very useful for commands that have sub-commands, and you want each sub-command to be implemented purely.
* A validation method does not do anything if the argument is valid, but throws an exception if it is not.
* A conversion or retreival method such as `Arguments.GetValue(key)` or `Arguments.GetValueAsInteger(key)` have an optional parameter named
`throwIfUnable` which is set to `false` by default. when it is set to `false` if any error arises, the value returned will be null.
but if set to `true`, an exception will be thrown if any error arises, but if the value is guaranteed to be not null.

### Usage tips and tricks

* If you want to a boolean arguments, as it is argument only but without value, such as `command --verbose`,
you don't need to retreive the value rather just use `Arguments.Validate("verbose")` this will throw an exception if the argument is not present.
* If you want something that is optional and has a default value, you can use the `Arguments.GetValue(key)` without the `throwIfUnable` parameter
to get the value only if present and valid, otherwise a null. Which means you can do something like this: `var val = Arguments.GetValue("key") ?? "default";`.
Same goes for all conversion varients of `GetValue` like `GetValueAsInteger` etc.
* If you have required arguments, make sure you put at least the parsing or retreiving method in a try catch block, and handle the exception.
* If you get your args from the main method such like this: `Main(string[] args)` you can pass the args paramater directly to `Parser.ParseArguments(args)`.
But make sure it is not empty. The result of `Parser.ParseArguments(args)` can be nullable and that is only if the arguments that were passed are empty.

## Source Code

from the point of public release, the master branch will only contain stable and tested code, so
to get the source code you can clone the master branch.