# Copyright (C) 2012-2014 Soomla Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#      http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

#!/usr/bin/python

from mod_pbxproj import *
from os import path, listdir
from shutil import copyfile
import sys

script_dir = os.path.dirname(sys.argv[0])
build_path = sys.argv[1]

frameworks = [
  'usr/lib/libsqlite3.0.dylib'
]

weak_frameworks = [

]

pbx_file_path = sys.argv[1] + '/Unity-iPhone.xcodeproj/project.pbxproj'
pbx_object = XcodeProject.Load(pbx_file_path)

def copysoomlalibs(libpath):
  target_soomla_binaries_path = path.join(build_path, 'Libraries', 'Soomla')
  if not os.path.isdir(target_soomla_binaries_path):
    os.mkdir(target_soomla_binaries_path)
  soomla_libraries_group = pbx_object.get_or_create_group('Soomla', parent=pbx_object.get_or_create_group('Libraries'))
  for library in os.listdir(libpath):
    name_components = library.split('.')
    if (name_components[len(name_components) - 1] == 'a'):
      copyfile(path.join(libpath, library), path.join(target_soomla_binaries_path, library))
      pbx_object.add_file_if_doesnt_exist(path.join(target_soomla_binaries_path, library), tree='SOURCE_ROOT', parent=soomla_libraries_group)
  return

soomla_binaries_path = path.join(script_dir, '..', '..', '..', 'Plugins', 'iOS', 'Soomla')
shared_binaries_path = path.join(script_dir, '..', '..', '..', 'Plugins', 'iOS', 'SoomlaShared')
copysoomlalibs(soomla_binaries_path)
copysoomlalibs(shared_binaries_path)

for framework in frameworks:
  pbx_object.add_file_if_doesnt_exist(framework, tree='SDKROOT')

for framework in weak_frameworks:
  pbx_object.add_file_if_doesnt_exist(framework, tree='SDKROOT', weak=True)

pbx_object.add_other_ldflags('-ObjC')

pbx_object.save()
