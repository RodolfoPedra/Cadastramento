using System;
using System.Globalization;
using System.Web.Mvc;


namespace Cadastramento.ModelData.Logic.ModelBinder
{
    public class IntModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext,
                                         ModelBindingContext bindingContext)
        {
            object result = null;

            // Don't do this here!
            // It might do bindingContext.ModelState.AddModelError
            // and there is no RemoveModelError!
            // 
            // result = base.BindModel(controllerContext, bindingContext);
            //modelValue.AttemptedValue
            string modelName = bindingContext.ModelName;
            var modelValue = bindingContext.ValueProvider.GetValue(modelName);
            string attemptedValue = modelValue != null
                ? (string.IsNullOrEmpty(modelValue.AttemptedValue) ? (bindingContext.ModelType.Name.Contains("Nullable") ? null : "0") : modelValue.AttemptedValue)
                : (bindingContext.ModelType.Name.Contains("Nullable") ? null : "0");

            if (attemptedValue == null)
                return null;

            // Depending on CultureInfo, the NumberDecimalSeparator can be "," or "."
            // Both "." and "," should be accepted, but aren't.
            string wantedSeperator = NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator;
            string alternateSeperator = (wantedSeperator == "," ? "." : ",");

            if (attemptedValue.IndexOf(wantedSeperator) == -1
                && attemptedValue.IndexOf(alternateSeperator) != -1)
            {
                attemptedValue =
                    attemptedValue.Replace(alternateSeperator, wantedSeperator);
            }

            try
            {
                if (bindingContext.ModelMetadata.IsNullableValueType
                    && string.IsNullOrWhiteSpace(attemptedValue))
                {
                    return null;
                }

                result = int.Parse(attemptedValue, NumberStyles.Any);
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e);
            }

            return result;
        }
    }
}
