namespace Wappa.Util.Flunt.Validations
{
    public partial class Contract
    {
        #region IsGreaterThan

        public Contract IsGreaterThan(decimal val, int comparer, string property, string message)
        {
            if ((double)val <= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(double val, int comparer, string property, string message)
        {
            if (val <= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(float val, int comparer, string property, string message)
        {
            if (val <= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterThan(int val, int comparer, string property, string message)
        {
            if (val <= comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsGreaterThan

        #region IsGreaterOrEqualsThan

        public Contract IsGreaterOrEqualsThan(decimal val, int comparer, string property, string message)
        {
            if ((double)val < comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(double val, int comparer, string property, string message)
        {
            if (val < comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(float val, int comparer, string property, string message)
        {
            if (val < comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsGreaterOrEqualsThan(int val, int comparer, string property, string message)
        {
            if (val < comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsGreaterOrEqualsThan

        #region IsLowerThan

        public Contract IsLowerThan(decimal val, int comparer, string property, string message)
        {
            if ((double)val >= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(double val, int comparer, string property, string message)
        {
            if (val >= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(float val, int comparer, string property, string message)
        {
            if (val >= comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerThan(int val, int comparer, string property, string message)
        {
            if (val >= comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsLowerThan

        #region IsLowerOrEqualsThan

        public Contract IsLowerOrEqualsThan(decimal val, int comparer, string property, string message)
        {
            if ((double)val > comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(double val, int comparer, string property, string message)
        {
            if (val > comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(float val, int comparer, string property, string message)
        {
            if (val > comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract IsLowerOrEqualsThan(int val, int comparer, string property, string message)
        {
            if (val > comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion IsLowerOrEqualsThan

        #region AreEquals

        public Contract AreEquals(decimal val, int comparer, string property, string message)
        {
            if ((double)val != comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(double val, int comparer, string property, string message)
        {
            if (val != comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(float val, int comparer, string property, string message)
        {
            if (val != comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreEquals(int val, int comparer, string property, string message)
        {
            if (val != comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion AreEquals

        #region AreNotEquals

        public Contract AreNotEquals(decimal val, int comparer, string property, string message)
        {
            if ((double)val == comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(double val, int comparer, string property, string message)
        {
            if (val == comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(float val, int comparer, string property, string message)
        {
            if (val == comparer)
                AddNotification(property, message);

            return this;
        }

        public Contract AreNotEquals(int val, int comparer, string property, string message)
        {
            if (val == comparer)
                AddNotification(property, message);

            return this;
        }

        #endregion AreNotEquals

        #region Between

        public Contract IsBetween(int val, int from, int to, string property, string message)
        {
            if (!(val > from && val < to))
                AddNotification(property, message);

            return this;
        }

        #endregion Between
    }
}