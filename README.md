# Arithmetics
Kata got from https://www.codurance.com/katalyst/arithmetics.

Developed with dotnet (c#) and Visual Studio.

## Practice objectives
- TDD

## Brief explanation
### Requirements
Create an application that helps Durance read the transactions of the cryptocurrency.
The transactions are arithmetic operations wrapped by parentheses. In case a record is invalid, we should let Durance know with an "Invalid record" error message.

### Rules
- All of the operations are wrapped in parentheses
- There is an even number of parentheses
- Spaces can be considered as separators (to help identify negative numbers)
- If only parenthesis are provided, return 0 (consider the other rules)
- Operations should follow PEMDAS precedence rules (Parentheses, Exponents, Multiplication/Division, Addition/Subtraction)

## Confession
I found it impossible to came up with the final algorithm using pure TDD. I got stuck many (I mean MANY!! :-)) times before I wrote a solution that could cover all the test cases. I'm not proud of the final code in terms on simplicity and readiness, but this was what I achieved so far.

I uploaded it not to show off, as there is no reason to do so (the algorithm is quite ugly and pretty complex to follow), but to provide someone with a solution to this kata, as I did not found any other one on the Internet.

In conclusion, what I practiced, in reality, was perserance and stuborness.