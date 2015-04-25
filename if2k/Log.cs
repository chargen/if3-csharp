using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InternetFilter.Log

{
    class Log

    {
        private TraceSource trace;
        private Object guard;
        private string prefix;
        public Log(Type class_type,string prefix)

        {
            this.trace = new TraceSource(class_type.FullName);
            this.guard = new Object();
            this.prefix = prefix;
        }
        public Log(string class_name,string prefix)

        {
            trace = new TraceSource(class_name);
            guard = new Object();
        }
        public void Error(int id)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Error, id, prefix);
            }
        }
        public void Error(int id, string fmt )

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Error, id, prefix + fmt );
            }
        }
        public void Error(int id, string fmt, params Object[] args)

        {
            lock (guard)

            {
                trace.TraceEvent( TraceEventType.Error, id, prefix +fmt, args );
            }
        }
        public void Information(int id)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Information, id, prefix);
            }
        }
        public void Information(int id, string fmt)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Information, id, prefix+fmt);
            }
        }
        public void Information(int id, string fmt, params Object[] args)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Information, id, prefix+fmt, args);
            }
        }
        public void Warning(int id)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Warning, id, prefix);
            }
        }
        public void Warning(int id, string fmt)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Warning, id, prefix+ fmt);
            }
        }
        public void Warning(int id, string fmt, params Object[] args)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Warning, id, prefix+fmt, args);
            }
        }

        public void Verbose(int id)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Verbose, id, prefix);
            }
        }
        public void Verbose(int id, string fmt)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Verbose, id, prefix + fmt);
            }
        }
        public void Verbose(int id, string fmt, params Object[] args)

        {
            lock (guard)

            {
                trace.TraceEvent(TraceEventType.Verbose, id, prefix + fmt, args);
            }
        }
    }
}
