﻿namespace Wappa.Util.Flunt.Validations
{
    public partial class Contract
    {
        #region IsGreaterThan

        public Contract IsGreaterThan(decimal val, decimal comparer, string property, string message)
        {
            if (val <= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(double val, decimal comparer, string property, string message)
        {
            if (val <= (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(float val, decimal comparer, string property, string message)
        {
            if (val <= (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(int val, decimal comparer, string property, string message)
        {
            if (val <= comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsGreaterThan

        #region IsGreaterOrEqualsThan

        public Contract IsGreaterOrEqualsThan(decimal val, decimal comparer, string property, string message)
        {
            if (val < comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(double val, decimal comparer, string property, string message)
        {
            if (val < (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(float val, decimal comparer, string property, string message)
        {
            if (val < (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(int val, decimal comparer, string property, string message)
        {
            if (val < comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsGreaterOrEqualsThan

        #region IsLowerThan

        public Contract IsLowerThan(decimal val, decimal comparer, string property, string message)
        {
            if (val >= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(double val, decimal comparer, string property, string message)
        {
            if (val >= (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(float val, decimal comparer, string property, string message)
        {
            if (val >= (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(int val, decimal comparer, string property, string message)
        {
            if (val >= comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsLowerThan

        #region IsLowerOrEqualsThan

        public Contract IsLowerOrEqualsThan(decimal val, decimal comparer, string property, string message)
        {
            if (val > comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(double val, decimal comparer, string property, string message)
        {
            if (val > (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(float val, decimal comparer, string property, string message)
        {
            if (val > (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(int val, decimal comparer, string property, string message)
        {
            if (val > comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsLowerOrEqualsThan

        #region AreEquals

        public Contract AreEquals(decimal val, decimal comparer, string property, string message)
        {
            if (val != comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(double val, decimal comparer, string property, string message)
        {
            if (val != (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(float val, decimal comparer, string property, string message)
        {
            if (val != (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(int val, decimal comparer, string property, string message)
        {
            if (val != comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion AreEquals

        #region AreNotEquals

        public Contract AreNotEquals(decimal val, decimal comparer, string property, string message)
        {
            if (val == comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(double val, decimal comparer, string property, string message)
        {
            if (val == (double)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(float val, decimal comparer, string property, string message)
        {
            if (val == (float)comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(int val, decimal comparer, string property, string message)
        {
            if (val == comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion AreNotEquals

        #region Between

        public Contract IsBetween(decimal val, decimal from, decimal to, string property, string message)
        {
            if (!(val > from && val < to))
                AddNotification(property, message);

            return this;
        }

        #endregion Between
    }
}