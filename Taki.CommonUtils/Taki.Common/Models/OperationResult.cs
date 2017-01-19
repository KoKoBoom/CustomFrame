/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/1/18 11:40:55
** Desc：	操作结果
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taki.Common
{
    public class OperationResult<T>
    {
        private EmOperationState state;
        public EmOperationState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                switch (state)
                {
                    case EmOperationState.INIT:
                        this.Message = "未发生任何操作";
                        break;
                    case EmOperationState.ERROR:
                        this.Message = "系统错误";
                        break;
                    case EmOperationState.FAIL:
                        this.Message = "操作失败";
                        break;
                    case EmOperationState.SUCCESS:
                        this.Message = "操作成功";
                        break;
                    default:
                        this.Message = "操作失败";
                        break;
                }
            }
        }
        /// <summary>
        /// 如果想给 Message 赋值  请先给 State 赋值   因为给 State 赋值时会修改 Messag 的值
        /// </summary>
        public string Message { get; set; } = "似乎出现一些问题";
        public T Data { get; set; }
    }

    public enum EmOperationState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        INIT,
        /// <summary>
        /// 操作失败
        /// </summary>
        FAIL,
        /// <summary>
        /// 系统异常
        /// </summary>
        ERROR,
        /// <summary>
        /// 操作成功
        /// </summary>
        SUCCESS
    }
}
