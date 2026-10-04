using Discount.Data;
using Discount.Models;
using Discount_Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger): DiscountProto.DiscountProtoBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                coupon = new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount Description",
                };
            }
            logger.LogInformation("Discount is retrived for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();


            logger.LogInformation("Discount is Created for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }


            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            var updatedCoupon = coupon.Adapt<CouponModel>();
            return updatedCoupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.ProductName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Request with ProductName={request.ProductName} is not Found"));
            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is Sucessfully deleted. ProductName: {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Sucess = true};
        }
    }
}
