from selenium import webdriver
from selenium.webdriver.common.keys import Keys
import time
# 打开Chrome浏览器
browser = webdriver.Chrome()
options = webdriver.ChromeOptions()
# 此步骤很重要，设置为开发者模式，防止被各大网站识别出来使用了Selenium  然而并没有卵用
# options.add_experimental_option('excludeSwitches', ['enable-automation'])
# options.add_experimental_option("prefs", {"profile.managed_default_content_settings.images": 2})
# browser.browser = webdriver.Chrome(executable_path="https://www.taobao.com", options=options)
# browser.add_experimental_option('excludeSwitches', ['enable-automation'])
# browser.add_experimental_option("prefs", {"profile.managed_default_content_settings.images": 2})
browser.get("https://www.taobao.com")
browser.find_element_by_link_text("亲，请登录").click()



# browser.find_element_by_id('J_OtherLogin').find_elements_by_tag_name('a')[0].click()
# browser.find_element_by_name('username').send_keys('15226533769')
# browser.find_element_by_name('password').send_keys('0.nishiwode')
# time.sleep(1)
# browser.find_element_by_name('password').send_keys(Keys.ENTER)


time.sleep(20)
browser.get("https://detail.tmall.com/item.htm?id=612904844942&spm=a1z2k.11010449.931864.2.6405509dwP7Hyu&scm=1007.13982.82927.0&last_time=1584080977&skuId=4314986868425")
tmp=browser.find_element_by_id("J_LinkBuy")
print(tmp.is_displayed())
# while 1:
#     try:
#         print('在监听')
#         browser.find_element_by_id("J_LinkBuy").click()
#         browser.find_element_by_link_text("结 算").click()
#         browser.find_element_by_link_text('提交订单').click()
#     except IOError:
#         print('休息一会再监听')
#         time.sleep(5)
#     else:
#         print('yes')
