using Buyablezone.PurchaseParams;

namespace Buyablezone.Abstract
{
    public abstract class ConditionHandler
    {
        protected ConditionHandler successor;

        public void SetSuccesor(ConditionHandler successor)
        {
            this.successor = successor;
        }

        public abstract void ProcessRequest(PurchaseParam purchase);
    }
}