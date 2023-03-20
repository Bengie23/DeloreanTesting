 # What is DeloreanTesting?
An extension of the existing MSTEST TestClass and TestMethod attributes, allowing to perform certain operations around the tests.
The idea is to acquire the next capabilities:
* Skip tests that might be too heavy, complex or useless for a certain context.
* Empower TDD by  allowing test to behave as a work in progress.
* Validate architectural decisions by testing that the expected code layers are in place.

Finally the goal is to empower architectural decisions by extending a well known practice as unit testing.

# Where does the naming come from?
Well, Delorean obviously comes from the BTTF movie, Copernicus and Einstein are the names of Doc's dogs. Einstein lives in the 'present' of 1985,
and Copernicus is Doc's companion during the 'past' of 1955.

# TestClasses && TestMethod attributes.
## Delorean
This is a test that at the moment is written would fail but will eventually pass.

## Copernicus
This is a test that will only run when the referenced Azure Devops ticket complies with a certain configuration.
### WorkItemState
Attaches the current testmethod/testclass to a workItemId from AzureDevops and only execute the test when the state of the workItem matches the given parameter

## Einstein
This is a test specialized in integration testing that depends on the result of another action/evaluation to determine if the test should pass or fail.
For example, a test that will validate if a certain type of object has been registered and/or served from DI during the operation.

# CustomAttributes
## ShouldSKip
Allows to skip a test when a certain condition is given.

### ShouldFail
Allows a test to pass, when it actually failed.

# Examples
