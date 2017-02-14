/********************************************************************************
** Auth：	Taki
** Mail:	mister_zheng@sina.com
** Date：	2017/2/13 1:33:09
** Desc：	
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taki.Common;
using Taki.Model;

namespace Taki.DAL
{
    public class UserDAL
    {
        public OperationResult<bool> Save(user model)
        {
            OperationResult<bool> result = new OperationResult<bool>() { State = EmOperationState.FAIL };
            using (takiEntities _db = new takiEntities())
            {
                var _model = _db.user.Where(x => x.Name == model.Name);
                if (_model != null && _model.Count() > 0)
                {
                    result.Message = "用户已存在";
                }
                else
                {
                    _db.user.Add(model);
                    result.Data = _db.SaveChanges() > 0;
                    result.State = result.Data ? EmOperationState.SUCCESS : EmOperationState.FAIL;
                }
            }
            return result;
        }

        public OperationResult<user> LoginIn(string name, string password)
        {
            OperationResult<user> result = new OperationResult<user>() { State = EmOperationState.FAIL };

            try
            {
                using (takiEntities _db = new takiEntities())
                {
                    var models = _db.user.Where(x => x.Name.Equals(name)).ToList();
                    if (models != null && models.Count() > 0)
                    {
                        if (models.Count() > 1)
                        {
                            result.Message = "存在多个相同账户";
                        }
                        else
                        {
                            if (models.First().Password.Equals(password))
                            {
                                if (models.First().Status == 1)
                                {
                                    result.Data = models.First();
                                    result.State = EmOperationState.SUCCESS;
                                }
                                else
                                {
                                    result.Message = "账户已被冻结，无法登录";
                                }
                            }
                            else
                            {
                                result.Message = "密码错误";
                            }
                        }
                    }
                    else
                    {
                        result.Message = "该用户不存在";
                    }
                }
            }
            catch (Exception)
            {
                result.State = EmOperationState.ERROR;
            }

            return result;
        }
    }
}
