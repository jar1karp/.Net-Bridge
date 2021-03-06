//
// General:
//      This file is pary of .NET Bridge
//
// Copyright:
//      2010 Jonathan Shore
//	2017 Jonathan Shore and Contributors
//
// License:
//      Licensed under the Apache License, Version 2.0 (the "License");
//      you may not use this file except in compliance with the License.
//      You may obtain a copy of the License at:
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//      Unless required by applicable law or agreed to in writing, software
//      distributed under the License is distributed on an "AS IS" BASIS,
//      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//      See the License for the specific language governing permissions and
//      limitations under the License.
//

// [[Rcpp::depends(BH)]]

#ifndef CLR_FLOAT64
#define CLR_FLOAT64

#include <cstdlib>
#include "msgs/CLRValue.hpp"

using namespace std;


//
// Float64 value
//
class CLRFloat64 : public CLRValue<double>
{
  public:

    CLRFloat64 (CLRApi* api, double* value = nullptr)
      : CLRValue(CLRMessage::TypeFloat64, api, value)
    {
    }

    // serialize object to stream
    void serialize (BufferedSocketWriter& stream)
    {
        assert (_value != NULL);
        CLRMessage::serialize (stream);
	stream.write_float64 (*_value);
    }

    // deserialize object from stream
    void deserialize (BufferedSocketReader& stream)
    {
        _value = new double;
	*_value = stream.read_float64();
    }
};

#endif
