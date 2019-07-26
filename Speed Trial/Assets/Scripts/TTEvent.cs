using System;

namespace ThirdTime.Utils
{
	public class TTEvent
	{
		public delegate void TheDelegate(); 
		event TheDelegate theEvent;

		public static TTEvent operator +(TTEvent lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent += rhs;
			}

			return lhs;
		}

		public static TTEvent operator -(TTEvent lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent -= rhs;
			}

			return lhs;
		}

		public void Raise()
		{

			if (theEvent != null)
			{
				foreach(TheDelegate handler in theEvent.GetInvocationList())
				{
					try
					{
						handler();
					}
					catch (Exception e)
					{
						// One of the handlers had a problem
						//Common.Logging.Logger.Exception(e);
					}
				}
			} 
		}
	}

	public class TTEvent<T>
	{
		public delegate void TheDelegate(T arg); 
		event TheDelegate theEvent;

		public static TTEvent<T> operator +(TTEvent<T> lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent += rhs;
			}

			return lhs;
		}

		public static TTEvent<T> operator -(TTEvent<T> lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent -= rhs;
			}

			return lhs;
		}

		public void Raise(T arg)
		{
			if (theEvent != null)
			{
				foreach(TheDelegate handler in theEvent.GetInvocationList())
				{
					try
					{
						handler(arg);
					}
					catch (Exception e)
					{
						// One of the handlers had a problem
						//Common.Logging.Logger.Exception(e);
					}
				}
			} 
		}
	}

	public class TTEvent<T,U>
	{
		public delegate void TheDelegate(T arg1, U arg2); 
		event TheDelegate theEvent;

		public static TTEvent<T,U> operator +(TTEvent<T,U> lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent += rhs;
			}

			return lhs;
		}

		public static TTEvent<T,U> operator -(TTEvent<T,U> lhs, TheDelegate rhs)
		{
			if (rhs != null)
			{
				lhs.theEvent -= rhs;
			}

			return lhs;
		}

		public void Raise(T arg1, U arg2)
		{
			if (theEvent != null)
			{
				foreach(TheDelegate handler in theEvent.GetInvocationList())
				{
					try
					{
						handler(arg1, arg2);
					}
					catch (Exception e)
					{
						// One of the handlers had a problem
						//Common.Logging.Logger.Exception(e);
					}
				}
			} 
		}
	}

    public class TTEvent<T, U, V>
    {
        public delegate void TheDelegate(T arg1, U arg2, V arg3);
        event TheDelegate theEvent;

        public static TTEvent<T, U, V> operator +(TTEvent<T, U, V> lhs, TheDelegate rhs)
        {
            if (rhs != null)
            {
                lhs.theEvent += rhs;
            }

            return lhs;
        }

        public static TTEvent<T, U, V> operator -(TTEvent<T, U, V> lhs, TheDelegate rhs)
        {
            if (rhs != null)
            {
                lhs.theEvent -= rhs;
            }

            return lhs;
        }

        public void Raise(T arg1, U arg2, V arg3)
        {
            if (theEvent != null)
            {
                foreach (TheDelegate handler in theEvent.GetInvocationList())
                {
                    try
                    {
                        handler(arg1, arg2, arg3);
                    }
                    catch (Exception e)
                    {
                        // One of the handlers had a problem
	                    //Common.Logging.Logger.Exception(e);
                    }
                }
            }
        }
    }
}