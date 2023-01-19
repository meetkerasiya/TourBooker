namespace TourBooker
{
    public class CountryCode : IComparable<CountryCode>
    {
        public string Value { get; }
        public CountryCode(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value;
        }
        //public override bool Equals(object? obj)
        //{
        //    if (obj is CountryCode countryCode)
        //    {
        //        return StringComparer.OrdinalIgnoreCase.Equals(Value, countryCode.Value);
        //    }
        //    return false;
        //}
        //public override int GetHashCode()
        //{
        //    return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Value);
        //}

        public int CompareTo(CountryCode? other)
        {
            if(! (other is CountryCode)) return 1;
            if(!other.Value.Equals(this.Value))
            {
                return this.Value.CompareTo(other.Value);
            }
            return 0;
            //this commented line won't work
            //return Convert.ToInt32(StringComparer.OrdinalIgnoreCase.Equals(this.Value, other.Value));
        }

        //public static bool operator ==(CountryCode countryCode1, CountryCode countryCode2)
        //{
        //    return countryCode1.Equals(countryCode2);
        //}
        //public static bool operator !=(CountryCode countryCode1, CountryCode countryCode2)
        //{
        //    return !countryCode1.Equals(countryCode2);
        //}
    }
}
