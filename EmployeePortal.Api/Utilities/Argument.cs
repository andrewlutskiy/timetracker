using System.Diagnostics;

namespace EmployeePortal.Api.Utilities;

public static class Argument
    {
        [DebuggerStepThrough]
        public static void ExpectNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNull(object argumentValue)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(nameof(argumentValue));
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNull<TKey, T>(IDictionary<TKey, T> dictionary, Func<T, string> value, string argumentName)
        {
            if (dictionary.Any(x => value(x.Value) == null))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new ArgumentException(argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNullOrWhiteSpace(string argumentValue)
        {
            ExpectNotNullOrWhiteSpace(argumentValue, nameof(argumentValue));
        }

        [DebuggerStepThrough]
        public static void ExpectNotNullOrWhiteSpace<TKey, T>(IDictionary<TKey, T> dictionary, Func<T, string> value, string argumentName)
        {
            if (dictionary.All(x => string.IsNullOrWhiteSpace(value(x.Value))))
            {
                throw new ArgumentException(argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotEmptyGuid(Guid value, string argumentName)
        {
            ExpectNotNull(value, argumentName);

            if (value == Guid.Empty)
            {
                throw new ArgumentException($"{argumentName} cannot be an empty Guid");
            }
        }

        [DebuggerStepThrough]
        public static void ExpectGreaterThanZero(decimal value, string argumentName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{argumentName} cannot be less or equal zero");
            }
        }

        [DebuggerStepThrough]
        public static void ExpectGreaterThanZero(long value, string argumentName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{argumentName} cannot be less or equal zero");
            }
        }

        [DebuggerStepThrough]
        public static void ExpectGreaterThanZero(decimal? value, string argumentName)
        {
            ExpectNotNull(value, argumentName);

            if (value <= 0)
            {
                throw new ArgumentException($"{argumentName} cannot be less or equal zero");
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNegative(decimal? value, string argumentName)
        {
            ExpectNotNull(value, argumentName);

            if (value < 0)
            {
                throw new ArgumentException($"{argumentName} cannot have negative value.");
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotNegative(decimal value, string argumentName)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{argumentName} cannot have negative value.");
            }
        }

        [DebuggerStepThrough]
        public static void Expect(Func<bool> condition, string message)
        {
            ExpectNotNull(condition, "condition");

            if (!condition())
            {
                throw new ArgumentException(message);
            }
        }

        [DebuggerStepThrough]
        public static void Expect<T>(Func<bool> condition, string message)
            where T : Exception, new()
        {
            ExpectNotNull(condition, "condition");

            if (!condition())
            {
                throw (T)Activator.CreateInstance(typeof(T), message);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectOnlyDigits(string value, string argumentName)
        {
            ExpectNotNull(value, argumentName);

            if (!value.ToArray().All(char.IsDigit))
            {
                throw new ArgumentException(argumentName);
            }
        }

        [DebuggerStepThrough]
        public static void ExpectNotEmpty<T>(IEnumerable<T> enumerable, string argumentName)
        {
            if (!enumerable.Any())
            {
                throw new ArgumentException($"{argumentName} cannot be empty.");
            }
        }
    }